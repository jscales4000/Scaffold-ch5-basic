# CH5-Svelte Scaffold Compatibility Guide

This document provides a comprehensive overview of our process in creating a compatible Svelte-based scaffold for Crestron CH5 touch panels. It details both the challenges encountered and their solutions.

## Table of Contents
1. [Project Overview](#project-overview)
2. [Initial Issues](#initial-issues)
3. [Technical Diagnosis](#technical-diagnosis)
4. [Compatibility Solutions](#compatibility-solutions)
5. [Implementation Process](#implementation-process)
6. [Success Criteria](#success-criteria)
7. [Best Practices](#best-practices)

## Project Overview

The goal was to create a reusable scaffold preset for CH5-Svelte projects that includes:
- Core layout components (header, footer, sidebar, viewport)
- Theme support with color palette
- Advanced UI patterns and layouts
- Proper build configuration for Crestron touchpanel compatibility

The scaffold needed to be based on the working `_CH5-Svelte525` project while implementing modern development practices.

## Initial Issues

Our scaffold initially **failed to display on the Crestron panel** despite successful build and .ch5z packaging. Specific issues included:

1. **No Visible Errors**: Panel logs showed no critical errors, but no evidence of UI initialization
2. **Silent Loading Failure**: Panel accepted the .ch5z file but displayed nothing
3. **JavaScript Execution Issues**: No evidence of JavaScript running on the panel

## Technical Diagnosis

Through comparative analysis between the working `_CH5-Svelte525` project and our scaffold, we identified these critical differences:

1. **Module Format Incompatibility**:
   - ❌ Scaffold used ES Modules (`<script type="module">`) which are **not supported** by Crestron panel browsers
   - ✅ Working project used classic JavaScript (IIFE/UMD format)

2. **Missing Required Crestron Files**:
   - ❌ Scaffold was missing `cr-com-lib.js` (Crestron Communication Library)
   - ❌ Missing or incorrectly located `contract.cse2j` file
   - ❌ Incomplete appui/manifest configuration

3. **Asset Path Issues**:
   - ❌ Absolute paths (`/assets/...`) don't work properly in the panel environment
   - ✅ Working project used relative paths (`./assets/...`)

## Compatibility Solutions

### 1. JavaScript Module Format

**Problem**: ES Modules aren't supported by Crestron panel browsers.

**Solution**: Configure Vite to output classic JavaScript format:
```javascript
// vite.config.mjs
export default defineConfig({
  // ...
  build: {
    target: 'es2015',
    rollupOptions: {
      output: {
        format: 'iife', // Non-module format
        // ...
      }
    }
  },
  // ...
});
```

### 2. Required Crestron Files

**Problem**: Missing critical Crestron runtime files.

**Solution**: Copy these files from the working project:
- `cr-com-lib.js` → root of dist folder
- `config/contract.cse2j` → same path in scaffold
- `appui/manifest` → same path in scaffold

### 3. HTML Structure

**Problem**: Module scripts and incorrect paths.

**Solution**: Update HTML to load scripts correctly:
```html
<!-- Before -->
<script type="module" crossorigin src="/assets/index-IYvmhTsM.js"></script>

<!-- After -->
<script src="./cr-com-lib.js"></script>
<script src="./assets/index-IYvmhTsM.js"></script>
```

### 4. Asset Paths

**Problem**: Absolute paths don't work in panel environment.

**Solution**: Configure Vite to use relative paths:
```javascript
// vite.config.mjs
export default defineConfig({
  // ...
  base: './',
  // ...
});
```

## Implementation Process

### Step 1: Copying Required Crestron Files
1. First, copy the critical Crestron library:
   ```powershell
   Copy-Item "c:\path\to\_CH5-Svelte525\dist\cr-com-lib.js" "c:\path\to\scaffold\dist\cr-com-lib.js" -Force
   ```

2. Create necessary directories and copy configuration files:
   ```powershell
   New-Item -Path "c:\path\to\scaffold\dist\config" -ItemType Directory -Force
   Copy-Item "c:\path\to\_CH5-Svelte525\dist\config\contract.cse2j" "c:\path\to\scaffold\dist\config\contract.cse2j" -Force
   
   New-Item -Path "c:\path\to\scaffold\dist\appui" -ItemType Directory -Force
   Copy-Item "c:\path\to\_CH5-Svelte525\dist\appui\manifest" "c:\path\to\scaffold\dist\appui\manifest" -Force
   ```

### Step 2: Updating Vite Configuration
Update `vite.config.mjs` to generate panel-compatible output:

```javascript
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
```

### Step 3: Modifying HTML Template
Update the HTML to load scripts correctly:

```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Touchpanel UI Scaffold</title>
    <link rel="icon" href="favicon.ico" />
    <script src="./cr-com-lib.js"></script>
    <script src="./assets/index-IYvmhTsM.js"></script>
    <link rel="stylesheet" href="./assets/index-BB4VkPPu.css">
  </head>
  <body>
    <div id="app"></div>
  </body>
</html>
```

### Step 4: Building and Packaging
1. Rebuild with the new configuration:
   ```
   npm run build
   ```

2. Package using ch5-cli:
   ```
   ch5-cli archive -p "svelte-scaffold-basic-1" -d "./dist" -o "./ch5z"
   ```

## Success Criteria

The scaffold now successfully:
- Builds with Vite in a Crestron-compatible format
- Includes all required Crestron libraries and configuration files
- Loads and displays correctly on Crestron touchpanels
- Maintains modern development workflow while ensuring compatibility

## Best Practices

To maintain compatibility with Crestron panels in Svelte/Vite projects:

1. **JavaScript Format**:
   - Always use IIFE output format, not ES Modules
   - Target ES2015 for browser compatibility

2. **Required Files**:
   - Ensure `cr-com-lib.js` is loaded before application code
   - Include proper `config/contract.cse2j` file
   - Maintain correct `appui/manifest` structure

3. **Asset References**:
   - Always use relative paths (`./`) not absolute (`/`)
   - Set Vite's `base: './'` configuration

4. **HTML Structure**:
   - Do not use `type="module"` in script tags
   - Load Crestron libraries before application code

5. **Build Process**:
   - Always validate the build output structure before packaging
   - Use `ch5-cli archive` command for packaging

By following these guidelines, future Svelte-based Crestron projects can avoid compatibility issues and ensure smooth deployment to touchpanel hardware.
