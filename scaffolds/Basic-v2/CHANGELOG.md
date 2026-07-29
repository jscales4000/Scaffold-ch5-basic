# Changelog

## [0.8.1 lite] - 2025-05-26
### Added
- Optimistic UI refactor: All button presses (including source selection) now update the UI immediately for snappy touchpanel feedback, sending commands to the control system in the background.
- Project-wide standard: Button interactions are optimistic by default unless otherwise specified.
- PRD updated to version 1.6 with detailed requirements and steps for instituting optimistic UI patterns project-wide.

### Changed
- Feedback polling now only overwrites local state if the control system feedback disagrees, ensuring reliability while maximizing perceived responsiveness.

### Documentation
- See `prd_detailed_v1.6.md` for full requirements and implementation steps.

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.7.1] - 2025-05-26

### Changed
- Refactored `SourceControlRouter.svelte` to remove duplicate `controlTypeComponents` declaration and centralize component mapping logic, improving maintainability and performance.
- Memoized derived state for `visibleSources`, `selectedSource`, and `visibleSourceIcons` in `App.svelte` to reduce unnecessary renders.
- Simplified conditional rendering logic in `SourceControlRouter.svelte` to minimize DOM complexity and improve runtime efficiency.
- Improved error handling and user feedback for unavailable or unknown control types.
- Fixed all known lint errors, including variable redeclarations and unnecessary reactivity.

### Technical
- Confirmed all optimizations are compatible with single-file builds and Crestron deployment requirements.
- Ran successful production builds and CH5 archives to validate all changes.

### Files Changed
- `src/components/source-control/SourceControlRouter.svelte`
- `src/App.svelte`
- `Project Log.md`
- `CHANGELOG.md`

## [0.5.1] - 2025-05-22

### Changed
- Updated WebXPanel host configuration
- Fixed script loading in index.html to properly handle Crestron libraries
- Generated CH5 archive for deployment to touch panels

## [0.5.0] - 2025-05-18

### Added
- Initial project setup with Svelte 5 and TypeScript
- Crestron CH5 integration with WebXPanel support
- Responsive design for TS-770 (1280x800) and TS-1070 (1920x1200) touch panels
- University of Arizona theming and branding
- Tailwind CSS for styling
- Basic UI components for AV control
