import { defineConfig } from 'vite';
import { svelte } from '@sveltejs/vite-plugin-svelte';

export default defineConfig({
  plugins: [svelte()],
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
