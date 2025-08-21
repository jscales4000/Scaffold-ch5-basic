# Scaffold Comparison Guide

## Overview
This document compares the different scaffold variants available in this repository.

## Available Scaffolds

### Basic-v1
- **Created**: August 21, 2025
- **Status**: ✅ Production Ready
- **Use Case**: Standard touch panel interfaces
- **Features**:
  - Fixed header (64px) with time, help, and settings
  - Left sidebar with source selection
  - Footer (100px) with power and volume controls
  - Professional shadowbox styling
  - Responsive design for TS-770/TS-1070
- **Best For**: Conference rooms, classrooms, basic AV control

### Basic-v2 (Planned)
- **Status**: 🔄 Future Development
- **Enhancements**: 
  - Contract integration support
  - Enhanced theming system
  - Additional component library
  - Improved accessibility

## Choosing the Right Scaffold

| Need | Recommended Scaffold |
|------|---------------------|
| Quick AV interface | Basic-v1 |
| Custom branding | Basic-v1 (customizable) |
| Contract integration | Basic-v2 (when available) |
| Advanced navigation | Advanced-Navigation (planned) |

## Migration Path

### From Basic-v1 to Basic-v2
1. Copy project structure
2. Update dependencies
3. Migrate custom components
4. Test contract integration

## Performance Comparison

| Scaffold | Bundle Size | Load Time | Memory Usage |
|----------|-------------|-----------|--------------|
| Basic-v1 | ~20KB | <1s | Low |
| Basic-v2 | TBD | TBD | TBD |