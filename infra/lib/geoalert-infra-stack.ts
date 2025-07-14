import { CfnOutput, Duration, Stack, StackProps } from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as lambda from 'aws-cdk-lib/aws-lambda';
import * as apigateway from 'aws-cdk-lib/aws-apigateway';
import * as dynamodb from 'aws-cdk-lib/aws-dynamodb';
import * as sns from 'aws-cdk-lib/aws-sns';
import * as events from 'aws-cdk-lib/aws-events';
import { RemovalPolicy } from 'aws-cdk-lib';
import * as iam from 'aws-cdk-lib/aws-iam';
import * as location from 'aws-cdk-lib/aws-location'; 

export class GeoAlertInfraStack extends Stack {
  constructor(scope: Construct, id: string, props?: StackProps) {
    super(scope, id, props);

    const geoAlertLambda = new lambda.Function(this, 'GeoAlertLambda', {
      runtime: lambda.Runtime.DOTNET_8,
      handler: 'GeoAlert.Api::GeoAlert.Api.LambdaEntryPoint::FunctionHandlerAsync',
      code: lambda.Code.fromAsset('../apps/api/GeoAlert.Api/bin/Release/net8.0/publish'),
      memorySize: 256,
      timeout: Duration.seconds(10),
    });

    // API Gateway REST
    new apigateway.LambdaRestApi(this, 'GeoAlertApi', {
      handler: geoAlertLambda,
      proxy: true,
      restApiName: 'GeoAlertApi',
    });

    // DynamoDB Table
    const geoFencesTable = new dynamodb.Table(this, 'GeoFencesTable', {
      tableName: 'GeoFencesTable',
      partitionKey: { name: 'Id', type: dynamodb.AttributeType.STRING },
      billingMode: dynamodb.BillingMode.PAY_PER_REQUEST,
      removalPolicy: RemovalPolicy.DESTROY, // default removal policy
    });
      console.log(`GeoFencesTable ARN: ${geoFencesTable.tableArn}`);
    geoFencesTable.grantReadWriteData(geoAlertLambda);

    // SNS Topic
    new sns.Topic(this, 'GeoAlertNotificationsTopic', {
      displayName: 'GeoAlert Notifications Topic',
      
    });

    // EventBridge Rule (sin targets aún)
    new events.Rule(this, 'GeoAlertEventRule', {
      eventPattern: {
        source: ['geoalert.app'],
        detailType: ['GeoAlertEvent'],
      },
    });
    // AWS Location Service Place Index
    const geoAlertPlaceIndex = new location.CfnPlaceIndex(this, 'GeoAlertPlaceIndex', {
      indexName: 'GeoAlertPlaceIndex',
      dataSource: 'Esri',
      pricingPlan: 'RequestBasedUsage',
      description: 'GeoAlert Place Index for geocoding user addresses.',
    });

    // Grant Lambda permission to use the Place Index
    geoAlertLambda.addToRolePolicy(new iam.PolicyStatement({
      actions: ['geo:SearchPlaceIndexForText'],
      resources: [
        `arn:aws:geo:${this.region}:${this.account}:place-index/${geoAlertPlaceIndex.indexName}`
      ],
    }));

  
    geoAlertLambda.addEnvironment('PLACE_INDEX_NAME', geoAlertPlaceIndex.indexName);
    // Outputs
    new CfnOutput(this, "GeoAlertPlaceIndexName", {
      value: geoAlertPlaceIndex.indexName!,
      description: 'the GeoAlert Place Index name',
    });

  }
}

