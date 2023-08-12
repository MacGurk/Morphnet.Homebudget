import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import fs from 'fs';
import path from 'path';
import { spawn } from 'child_process';

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

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFileName)) {
  spawn(
    'dotnet',
    [
      'dev-certs',
      'https',
      '--export-path',
      certFilePath,
      '--format',
      'Pem',
      '--no-password',
    ],
    { stdio: 'inherit' },
  ).on('exit', (code) => process.exit(code));
}

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    port: 5002,
    https: {
      key: fs.readFileSync(keyFileName),
      cert: fs.readFileSync(certFilePath),
    },
    proxy: {
      '/api': {
        target: 'https://localhost:5001',
        changeOrigin: true,
      },
    },
  },
});
