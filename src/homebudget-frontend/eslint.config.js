import { defineConfig } from 'eslint/config';
import globals from 'globals';
import pluginVue from 'eslint-plugin-vue';
import eslintPluginPrettierRecommended from 'eslint-plugin-prettier/recommended';

export default defineConfig([
  ...pluginVue.configs['flat/recommended'],
  {
    languageOptions: {
      sourceType: 'module',
      globals: {
        ...globals.browser,
      },

      parserOptions: {
        parser: '@typescript-eslint/parser',
      },
    },

    rules: {
      'vue/multi-word-component-names': 'off',

      'vue/valid-v-slot': [
        'error',
        {
          allowModifiers: true,
        },
      ],

      'no-unused-vars': [
        'warn',
        {
          argsIgnorePattern: '^_',
        },
      ],

      quotes: [
        2,
        'single',
        {
          avoidEscape: true,
        },
      ],
    },
  },
  eslintPluginPrettierRecommended,
]);
