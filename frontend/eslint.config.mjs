import pluginNext from '@next/eslint-plugin-next';
import { defineConfig } from "eslint/config";
import js from "@eslint/js";
import tseslint from "typescript-eslint";

export default defineConfig([
  {
    ignores: [
      "node_modules/**",
      ".next/**",
      "out/**",
      "build/**",
      "next-env.d.ts",
      "__tests__/**",
      "**/*.test.*",
      "**/*.stories.*",
    ],
  },
  // Base configs (recommended)
  js.configs.recommended,
  tseslint.configs.recommended,
  // General rules for all source files
  {
    files: ["**/*.{js,jsx,ts,tsx}"],
    plugins: {
      '@next/next': pluginNext,
    },
    languageOptions: {
      parser: tseslint.parser,
      parserOptions: {
        ecmaVersion: "latest",
        sourceType: "module",
        ecmaFeatures: {
          jsx: true,
        },
        project: "./tsconfig.json",
      },
    },
    rules: {
      // Next.js rules
      ...pluginNext.configs.recommended.rules,
      ...pluginNext.configs['core-web-vitals'].rules,
      "prefer-const": "error",
      "no-var": "error",
      eqeqeq: ["error", "smart"],
      // Disable the console rule globally
      "no-console": "off",
      // Semicolon config
      semi: "warn",
      // TypeScript related configuration
      "@typescript-eslint/no-explicit-any": "off",
      "@typescript-eslint/no-unused-expressions": "off",
      "@typescript-eslint/no-unused-vars": "warn",
      "@typescript-eslint/no-empty-object-type": "off",
    },
  },
]);
