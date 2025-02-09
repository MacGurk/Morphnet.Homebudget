import { UserConfig, defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import * as path from 'path';
import * as fs from 'fs';

const baseFolder =
  process.env.APPDATA !== undefined && process.env.APPDATA !== ''
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`;
('/home/omorf/.dg-cli/cryptography/dev-certs/');

const certificateArg = process.argv
  .map((arg) => arg.match(/--name=(?<value>.+)/i))
  .filter(Boolean)[0];
const certificateName = certificateArg
  ? certificateArg.groups.value
  : process.env.npm_package_name;

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFileName = path.join(baseFolder, `${certificateName}.key`);

export default defineConfig(async () => {
  const config: UserConfig = {
    plugins: [vue()],
    resolve: {
      alias: {
        '@': path.resolve(__dirname, './src'),
      },
    },
    server: {
      port: 5002,
      proxy: {
        '/api': {
          target: 'https://localhost:5001',
          changeOrigin: true,
          secure: false,
        },
      },
    },
  };

  if (fs.existsSync(certFilePath) || fs.existsSync(keyFileName)) {
    config.server.https = {
      key: fs.readFileSync(keyFileName),
      cert: fs.readFileSync(certFilePath),
    };
  }

  return config;
});
