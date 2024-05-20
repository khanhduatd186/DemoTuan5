import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44332/',
  redirectUri: baseUrl,
  clientId: 'DemoTuan5_App',
  responseType: 'code',
  scope: 'offline_access DemoTuan5',
  requireHttps: true
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'DemoTuan5',
    logoUrl: '',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44332',
      rootNamespace: 'DemoTuan5',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
    DemoTuan5: {
      url: 'https://localhost:44348',
      rootNamespace: 'DemoTuan5',
    },
  },
} as Environment;
