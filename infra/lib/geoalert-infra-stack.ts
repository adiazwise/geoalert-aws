import { Stack, StackProps } from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as lambda from 'aws-cdk-lib/aws-lambda';
import * as apigateway from 'aws-cdk-lib/aws-apigateway';
import * as dynamodb from 'aws-cdk-lib/aws-dynamodb';
import * as sns from 'aws-cdk-lib/aws-sns';
import * as events from 'aws-cdk-lib/aws-events';
import { RemovalPolicy } from 'aws-cdk-lib';

export class GeoAlertInfraStack extends Stack {
  constructor(scope: Construct, id: string, props?: StackProps) {
    super(scope, id, props);

    // Lambda Function
    const geoAlertLambda = new lambda.Function(this, 'GeoAlertLambda', {
      runtime: lambda.Runtime.NODEJS_20_X, // Cambia a .NET cuando tengas el código listo
      handler: 'index.handler',
      code: lambda.Code.fromInline(`
        exports.handler = async (event) => {
          console.log("Received event:", JSON.stringify(event));
          return { statusCode: 200, body: "Hello from Lambda" };
        };
      `),
    });

    // API Gateway REST
    new apigateway.LambdaRestApi(this, 'GeoAlertApi', {
      handler: geoAlertLambda,
      proxy: true,
      restApiName: 'GeoAlertApi',
    });

    // DynamoDB Table
    new dynamodb.Table(this, 'GeoFencesTable', {
      partitionKey: { name: 'Id', type: dynamodb.AttributeType.STRING },
      billingMode: dynamodb.BillingMode.PAY_PER_REQUEST,
      removalPolicy: RemovalPolicy.DESTROY, // default removal policy
    });

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
  }
}

