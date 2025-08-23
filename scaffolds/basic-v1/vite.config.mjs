import { defineConfig } from 'vite';
import { svelte } from '@sveltejs/vite-plugin-svelte';
import { vitePreprocess } from '@sveltejs/vite-plugin-svelte';
import { viteStaticCopy } from 'vite-plugin-static-copy';

export default defineConfig({
  plugins: [
    svelte({ preprocess: vitePreprocess() }),
    // Copy CrComLib to build directory
    viteStaticCopy({
      targets: [
        {
          src: 'node_modules/@crestron/ch5-crcomlib/build_bundles/umd/cr-com-lib.js',
          dest: ''
        }
      ]
    })
  ],
  server: {
    port: 5173
  },
  build: {
    // Use classic non-module output format for Crestron panel compatibility
    target: 'es2015',
    outDir: 'dist',
    rollupOptions: {
      output: {
        format: 'iife', // Use immediately-invoked function expression instead of ESM
        entryFileNames: 'assets/[name]-[hash].js',
        chunkFileNames: 'assets/[name]-[hash].js',
        assetFileNames: 'assets/[name]-[hash].[ext]'
      }
    }
  },
  // Use relative base path for asset URLs
  base: './'
});
