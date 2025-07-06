#!/usr/bin/env node
import * as cdk from 'aws-cdk-lib';
import { GeoAlertInfraStack } from '../lib/geoalert-infra-stack';
import { App } from 'aws-cdk-lib';

const app = new App();
new GeoAlertInfraStack(app, 'GeoAlertInfraStack', {});

