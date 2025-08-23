import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import { viteStaticCopy } from 'vite-plugin-static-copy';
import { viteSingleFile } from 'vite-plugin-singlefile';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    svelte(),

    //Add this to copy the cr-com-lib.js file to the build directory
    viteStaticCopy({
      targets: [
        {
          src: 'node_modules/@crestron/ch5-crcomlib/build_bundles/umd/cr-com-lib.js',
          dest: ''
        }
      ]}),

    // Single-file plugin enabled for touchpanel compatibility
    viteSingleFile(),
  ],
  build: {
    sourcemap: false,
    minify: 'esbuild',
    cssCodeSplit: false,
    rollupOptions: {
      output: {
        // No manualChunks: single-file output only
      }
    }
  }
})