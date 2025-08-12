import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44344/',
  redirectUri: baseUrl,
  clientId: 'Cts_App',
  responseType: 'code',
  scope: 'offline_access Cts',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Cts',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44344',
      rootNamespace: 'Cts',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
