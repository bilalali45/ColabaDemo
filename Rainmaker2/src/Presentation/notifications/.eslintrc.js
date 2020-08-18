module.exports = {
  root: true,
  env: {
    node: true
  },
  settings: {
    react: {
      createClass: 'createReactClass',
      pragma: 'React',
      version: 'detect'
    }
  },
  extends: [
    'eslint:recommended',
    'plugin:@typescript-eslint/eslint-recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:react/recommended',
    'prettier',
    'prettier/react'
  ],
  parser: '@typescript-eslint/parser',
  plugins: ['react', '@typescript-eslint', 'react-hooks', 'unused-imports'],
  rules: {
    '@typescript-eslint/no-unused-vars': 'error',
    'unused-imports/no-unused-imports-ts': 'error',
    '@typescript-eslint/explicit-module-boundary-types': 'error',
    'react/prop-types': 0,
    'unused-imports/no-unused-vars-ts': [
      'warn',
      {
        vars: 'all',
        varsIgnorePattern: '^_',
        args: 'after-used',
        argsIgnorePattern: '^_'
      }
    ],
    'react-hooks/rules-of-hooks': 'error',
    'react-hooks/exhaustive-deps': 'error',
    '@typescript-eslint/no-use-before-define': [
      'error',
      {functions: true, classes: true, variables: false}
    ]
  }
};
