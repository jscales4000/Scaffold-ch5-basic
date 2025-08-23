# Project Log

## v0.8.6-lite — Routing Pattern & Inset Shadow Standard (2025-05-27)

### Overview
This release establishes a new routing system for Crestron touchpanel source control components, setting the standard for all future touchpanel UI features. The routing pattern in `SourceControl.svelte` is now the preferred approach for mapping control types to components. Additionally, the main source control container now uses an inverted (inset) box shadow for a modern, sunken visual effect.

---

### Key Features & Standards

#### 1. **Source Control Routing System**
- The routing logic in `SourceControl.svelte` dynamically maps control type signals to the correct source control component.
- This system is now the project standard for all Crestron touchpanel UIs, ensuring easy extensibility for future sources and features.
- All new features requiring routing or dynamic component rendering should follow this pattern.

#### 2. **Inset (Inverted) Box Shadow**
- `.source-control-container` now uses a fully inset box-shadow for a "sunken" effect, improving visual clarity and modernizing the UI.
- This style is recommended for all shadowbox containers in future features.

#### 3. **Versioning & Documentation**
- Project version updated to `0.8.6-lite` in `package.json`.
- This log entry documents the new routing standard and visual update.

---

### Summary of Accomplishments
- Implemented and validated the new routing pattern in `SourceControl.svelte`.
- Updated CSS to use an inset box-shadow for the main container.
- Established these changes as the baseline for all future Crestron touchpanel UI development.
- Pushed all changes to GitHub with version tag `v0.8.6-lite`.

#### Next Steps
- Continue to build new features using the documented routing and styling standards.
- Expand the routing system as new source types and control features are added.

---

## v0.8.5-lite — CH5 UI Layout & Overflow Standards (2025-05-27)

### Overview
This release standardizes all layout, spacing, and overflow settings for CH5 Svelte UI projects, ensuring perfect, consistent rendering across WebXPanel, TS-770, and all target touchpanel environments. The following best practices and technical documentation are to be followed for all future CH5 projects.

---

### Key Settings & Best Practices

#### 1. **No-Scroll Policy**
- **All control panels and shadowboxes must have:**
  - `overflow: hidden` or `overflow-y: hidden` on all containers and content panes.
  - No container should use `overflow-y: auto` or `overflow: auto` unless explicitly required for a non-control, non-shadowbox element.
  - All scrollbars are disabled for a fixed, touch-friendly layout.

#### 2. **Spacing and Layout**
- **Source control containers** fill the display area with zero or minimal (edge-to-edge) padding, enforced globally with selectors such as:
  - `#app-container > main > div.content-area... > div.display-area... > div`
- **Shadowbox containers** use precise margin and padding for visual balance. Example:
  - `margin: 17px auto;`
  - `padding: 0 0;` (or as required by the specific layout)

#### 3. **Component Structure**
- All major containers use `display: flex` for layout, with `align-items` and `justify-content` set for central alignment unless overridden for a specific UX reason.
- `.controls-content` and similar classes must:
  - Use `flex: 1`, `max-height: 100%`, and `border-radius: 0.25rem`.
  - Never allow content to overflow or scroll.

#### 4. **Color & Theme**
- **Primary Brand Colors:**
  - Arizona Cardinal: `#AB0520`
  - Arizona Blue: `#0C234B`
- **Shadowbox backgrounds** should use solid hex values only (never rgba or alpha), e.g. `background: #0C234B;` or `background: #AB0520;` for special states.
- **Borders and shadows:**
  - `box-shadow: 0 10px 15px -3px rgba(0,0,0,0.3), 0 4px 6px -2px rgba(0,0,0,0.2), inset 0 2px 4px rgba(255,255,255,0.1), 0 0 0 1px rgba(255,255,255,0.1);`
  - `border: 1px solid rgba(255,255,255,0.1);`
  - `border-radius: 0.75rem` (standard), or as specified for special containers.

#### 5. **Touchpanel Compatibility**
- All colors, radii, and layout rules are tested for TS-770 and WebXPanel rendering.
- **No transparency** in backgrounds for compatibility.
- Borders and shadows are always visible for clear container separation.

#### 6. **Theme Color Reference**
- The full UA color palette is available in Settings > Theme Colors tab (password: 1234). All colors display their hex values and names.

---

### Technical Documentation: How to Achieve This Layout

- **Disable all scrolling:**
  - Set `overflow: hidden` on all relevant containers in Svelte, CSS, and Tailwind classes.
  - Remove or override any `overflow-y: auto` or `overflow: auto` in global or component styles.
- **Padding and Margin:**
  - Use `padding: 0 0;` and `margin-top: 0;` on `.controls-content` and related containers.
  - All edge padding should be set to 0 unless a specific visual gap is required.
- **Component Flex Layout:**
  - Use `display: flex; align-items: center; justify-content: center; flex: 1;` for main content containers.
- **Color Application:**
  - Use only solid hex codes for backgrounds and borders as per UA branding.
- **Box Shadows and Borders:**
  - Apply standardized box-shadow and border settings for all shadowboxes and containers.

---

### Summary of Changes in v0.8.5-lite
- Removed all scrollbars and overflow from control panels, shadowboxes, and demo pages.
- Standardized all spacing, padding, and margin for edge-to-edge, touch-friendly layouts.
- Enforced UA brand colors and solid backgrounds for all containers.
- Updated shadowbox and container structure for perfect alignment and visual clarity.
- Documented all best practices and technical settings for future CH5 projects.

---

**All CH5 projects must follow these standards for consistent, professional, and touch-optimized UI layouts.**

---

*If you need to add new layouts or components, refer to this documentation to ensure full compliance with the UA CH5 UI standards.*


## Version 0.8.2 lite — 2025-05-26

### Resolved repeated rendering of source buttons and silenced debug output

**Date:** 2025-05-26

#### Summary of Accomplishments
- Disabled all debug output related to source button rendering and touchpanel join feedback in `App.svelte`.
- Refined feedback handler logic to ensure `currentSource` is only updated when the feedback value changes, eliminating unnecessary Svelte reactivity cycles.
- Confirmed that repeated rendering of source buttons is now fixed and logs are clean.
- Improved UI performance and maintainability.

#### Rationale
- This change resolves a critical UI performance issue and reduces unnecessary debug output, improving the overall user experience and development efficiency.

#### Technical Implementation
- Updated `App.svelte` to disable debug logging for source button rendering and touchpanel join feedback.
- Refactored feedback handler logic to optimize reactivity and performance.

#### Next Steps
- Continue to monitor and optimize UI performance for a seamless user experience.
- Address any remaining issues or bugs that may impact project stability.

---

## Version 0.8.1 lite — 2025-05-26

### Optimistic UI Refactor for Immediate Feedback

**Date:** 2025-05-26

#### Summary of Accomplishments
- Implemented an optimistic UI refactor for all button presses, ensuring immediate visual feedback for enhanced user experience.
- Updated source selection and interactive button logic to reflect this new pattern, providing a seamless and responsive interface.
- Integrated feedback polling to handle potential discrepancies between local state and control system responses, maintaining reliability and accuracy.

#### Rationale
- This upgrade aligns with the project's goal of delivering a modern, touch-optimized UI that meets the latest Crestron touchpanel standards.
- Optimistic UI handling is now a project-wide requirement, unless explicitly specified otherwise, to ensure consistency and responsiveness across all interactive elements.

#### Technical Implementation
- Refactored button press logic to update the UI instantly, sending commands to the control system asynchronously.
- Implemented a syncing state to facilitate future visual indicators for ongoing processes.
- Updated the PRD with detailed specifications for optimistic UI adoption, including optimization requirements for touchpanel responsiveness.

#### Next Steps
- Institute this pattern across the codebase by reviewing and updating all interactive elements to adhere to the new optimistic UI standard.
- Ensure all future development aligns with this standard, referencing the appended PRD specification for guidance.

#### References
- See `prd_detailed_v1.6.md` for the new section on Optimistic UI for Touchpanel Responsiveness.

---

## Version 0.7.1 — 2025-05-26

### Performance Optimization & Refactor Release

**Date:** 2025-05-26

#### Summary of Accomplishments
- **SourceControlRouter.svelte:**
  - Removed duplicate declaration of `controlTypeComponents` to resolve lint errors and ensure a single source of truth for component mapping.
  - Centralized and memoized the logic for determining which source control component to render, reducing unnecessary reactivity and improving performance.
  - Simplified the template to conditionally render only the enabled component, reducing DOM complexity.
- **App.svelte:**
  - Memoized derived state for `visibleSources`, `selectedSource`, and `visibleSourceIcons` to minimize unnecessary renders.
  - Fixed redeclaration issues for `selectedSource`.
- **General:**
  - Improved error handling and user feedback for unavailable or unknown control types.
  - Maintained strict adherence to runtime component configuration via Crestron signals, ensuring only enabled components are rendered.
  - Confirmed all optimizations are compatible with single-file builds and Crestron deployment requirements.

#### Rationale
- These changes significantly improve runtime performance by reducing unnecessary Svelte reactivity and DOM updates.
- Codebase is now easier to maintain, with clearer logic for component rendering and configuration.
- Lint errors are resolved, ensuring a cleaner development and build process.

#### Technical Implementation
- Refactored `SourceControlRouter.svelte` to have a single, authoritative `controlTypeComponents` mapping.
- Consolidated render logic into a single reactive statement for clarity and efficiency.
- Updated `App.svelte` for better state management and to eliminate redundant declarations.
- Ran successful production builds and CH5 archives to validate all changes.

#### Files Changed
- `src/components/source-control/SourceControlRouter.svelte`: render logic refactor, duplicate variable removal
- `src/App.svelte`: memoization, lint fixes
- `Project Log.md`: release notes for v0.7.1
- `CHANGELOG.md`: changelog entry for v0.7.1

### Changelog (see CHANGELOG.md for details)

## Version 0.7.0 — 2025-05-26 (Source_Start)

### Source Control Routing & Blank PC Test PAC

**Date:** 2025-05-26

#### Summary of Accomplishments
- Established a standardized, non-overflowing, fixed-size layout for all source control pages using `BaseSourceControl` and `SourceControlRouter`.
- Created a blank test PAC page for Source 1 (PC) as `PCSourceTest.svelte`, displaying only a single centered line of text, to serve as a template for future source pages.
- Updated routing in `SourceControlRouter.svelte` so that Source 1 now loads the new test PAC page, confirming the routing and layout infrastructure is in place for all sources.
- Confirmed that all source control pages will inherit the same background, viewport size, and no-scroll/overflow policy for visual and functional consistency.
- Bumped project version to 0.7.0 to mark the start of the source control implementation phase.

#### Rationale
- Provides a robust foundation for building out source-specific control pages with guaranteed layout and style consistency.
- Ensures that all new sources can be added with minimal setup and will conform to project standards for touchpanel compatibility and user experience.
- The test PAC for PC (Source 1) allows rapid validation of routing and layout before building out full-featured source controls.

#### Technical Implementation
- Added `PCSourceTest.svelte` as a minimal, layout-compliant source control page.
- Imported and routed `PCSourceTest` for Source 1 in `SourceControlRouter.svelte` (controlType 1).
- Confirmed that all source control containers remain non-scrollable and fixed-size as per PRD and project memories.
- Ran a successful production build (`npm run build`) to verify all changes.

#### Files Changed
- `src/components/source-control/PCSourceTest.svelte`: new blank test PAC for Source 1 (PC)
- `src/components/source-control/SourceControlRouter.svelte`: import and routing update for Source 1
- `package.json`: version bumped to 0.7.0
- `Project Log.md`: release notes added for v0.7.0

---

## Version 0.6.1 — 2025-05-25

### Modal Layout, Overflow, and Button Visual Cleanup

**Date:** 2025-05-25

#### Summary of Accomplishments
- **Removed all fixed width/height classes** (`w-[1807px]`, `h-[353px]`) from modal containers and related components to ensure sizing is controlled only by the correct device-specific classes (e.g., `w-[800px]`, `md:w-[1807.5px]`).
- **Cleaned up unused custom CSS** for those fixed width/height classes in the stylesheet, reducing code bloat and preventing accidental overrides.
- **Removed box-shadow from all button states** in the global stylesheet for a flatter, cleaner button appearance in line with updated UI direction.
- **Verified modal container overflow and sizing** to ensure no unwanted scrollbars or content spill, and that the modal remains visually consistent across both TS-770 and TS-1070 device layouts.
- **Confirmed DOM structure** remains performant and within best practices for element count and depth, supporting fast load and render times on touchpanels.

#### Rationale
- These changes directly support the project’s requirements for a fixed-size modal settings page, robust overflow handling, and a visually consistent, modern UI across all device targets.
- Cleaning up unused or redundant CSS and utilities helps maintainability and reduces the risk of future layout bugs.

#### Next Steps
- Begin source control panel refactor and audit for consistency, touch optimization, and visual polish.
- Continue accessibility improvements and address any remaining lint warnings.
- Document any new design patterns or standards that emerge during source control work.

---

## Version 0.6.0 — 2025-05-24

### UI Consistency, Shadowbox, and Touchpanel Compatibility Audit

**Date:** 2025-05-24

#### Summary
- Audited and refactored all source control panels to ensure strict UI consistency between WebXPanel and physical touchpanel (TS-770).
- Applied shadowbox styling to all relevant containers using the following design standard:
  - **Background:** `#0A1C3A` (explicit hex for touchpanel compatibility)
  - **Box shadow:** `0 10px 15px -3px rgba(0, 0, 0, 0.3), 0 4px 6px -2px rgba(0, 0, 0, 0.2), inset 0 2px 4px rgba(255, 255, 255, 0.1), 0 0 0 1px rgba(255, 255, 255, 0.1)`
  - **Border:** `1px solid rgba(255, 255, 255, 0.1)`
  - **Border radius:** `0.75rem`
- All inner containers (primary-controls, dpad-controls, color-buttons, camera list, etc.) now use `#0A1C3A` for backgrounds, overriding any variable or rgba value.
- Ensured border, box-shadow, and border-radius match the shadowbox design standard for all panels.
- Removed all volume controls (UI, logic, handlers) from the Media Player panel per updated requirements.
- Verified that all padding and layout rules enforce edge-to-edge content as specified in the PRD and memories.
- Confirmed that all changes are compatible with single-file deployment and do not rely on runtime web resources.

#### References
- See project memories for design standards, color preferences, and PRD-aligned layout rules.
- All changes align with the following requirements:
  - [Shadow box styling preference](#)
  - [Key styling requirements for the control panels](#)
  - [CSS layout enforcement for source-control panels](#)

#### Next Steps
- Test on both WebXPanel and physical touchpanel for final visual confirmation.
- Extend audit to additional panels if needed.
- Update documentation and PRD as new requirements emerge.

---

## v0.5.13 (2025-05-24)

### Major Accomplishments
- **Source-Control Container Zero Padding Policy**
    - All source-control display containers now have zero padding applied via CSS, ensuring content is flush with the container edges for every source type.
    - Enforced with the following selectors and `!important`:
      - `#app-container > main > div.content-area.s-XsEmFtvddWTw > div.display-area.s-XsEmFtvddWTw > div`
      - `#app-container > main > div.content-area.s-XsEmFtvddWTw > div.display-area.s-XsEmFtvddWTw > div > div > div`

### Rationale
- Guarantees a consistent, edge-to-edge UI layout across all AV source panels.
- Maximizes usable screen space for controls and content.
- Reduces visual clutter and supports a modern, touch-optimized design.
- Ensures that future sources/components will inherit this policy automatically.

### Technical Implementation
- CSS rules use `padding: 0 !important;` at both container levels to override any inherited or component-specific padding.
- This policy is now a project standard and will be referenced in the PRD for all new and existing source-control components.

### Design/UX Impact
- All AV control panels present a unified, professional appearance.
- Touch targets and content areas are maximized for usability on Crestron TS-770/TS-1070 panels.

---

## v0.5.12 (2025-05-24)

### Major Accomplishments
- **Lecture Capture UI Refactor**
    - Added tabbed buttons (Preview, Record/Stop, Camera) below the duration timer in the recording preview, styled with project blue (rgb(35 51 78)).
    - Center tab toggles between record and stop, reflecting recording state with icon and label.
    - Ensured tab buttons are responsive and fit the preview container.
- **Audio Controls Relocation**
    - Moved audio controls to the bottom right of the grid (grid-area: audio) for a more intuitive layout.
    - Audio controls styled for consistency with the rest of the UI.
- **Consistent Blue Theming**
    - Standardized usage of project blues: #0A1C3A, rgb(35 51 78), rgba(12, 35, 73, 1), etc.
    - Updated `.control-section`, `.recording-preview`, and tab/button backgrounds for visual harmony.
- **Shadowbox & Control Section Updates**
    - Ensured all shadowbox and control-section elements use consistent border-radius, box-shadow, and border styling.
    - Cleaned up and refactored related CSS selectors for maintainability.

### Files Changed
- `LectureCaptureSource.svelte`: UI structure, tab buttons, audio controls relocation
- `shadowbox.css`: Blue color updates, tab/button/control-section styling, shadowbox consistency

### Notes
- All changes are touch-optimized and tested for panel responsiveness.
- This release is a major step toward a cohesive, modern, and maintainable AV control UI for Crestron touch panels.

---

## [0.5.2] - 2025-05-25T16:05:56-07:00

### Highlights
- Fixed back-button navigation by forwarding click events in `ButtonSecondaryTemplate.svelte`.
- Removed red borders from secondary buttons via `app.css` (border set to transparent).
- Standardized icon colors to white by updating global text color (`--color-muted`) and CSS for `<i>` elements.
- Clarified and extended theme variables (`--color-surface`, `--color-surface-alt`, callout/border vars).
- Optimized build: removed single-file bundler, enabled code splitting, separated `vendor` and `crcomlib` bundles in `vite.config.ts`.
- Bumped package version from `0.5.1` to `0.5.2`.

### Changelog
- `ButtonSecondaryTemplate.svelte`: added `on:click` for event forwarding.
- `app.css`: updated color variables (`--color-accent`, `--color-muted`), added callout border vars.
- `vite.config.ts`: removed `viteSingleFile`, added `build.rollupOptions.manualChunks`.
- `package.json`: version bumped to `0.5.2`.
- `Project Log.md`: appended new release notes.

*Built with `npm run build` — confirmed separate chunks: `vendor-*.js`, `crcomlib-*.js`, CSS files.*

---

## Version 0.7.0 — 2025-05-26 (Source_Start)

### Source Control Routing & Blank PC Test PAC

**Date:** 2025-05-26

#### Summary of Accomplishments
- Established a standardized, non-overflowing, fixed-size layout for all source control pages using `BaseSourceControl` and `SourceControlRouter`.
- Created a blank test PAC page for Source 1 (PC) as `PCSourceTest.svelte`, displaying only a single centered line of text, to serve as a template for future source pages.
- Updated routing in `SourceControlRouter.svelte` so that Source 1 now loads the new test PAC page, confirming the routing and layout infrastructure is in place for all sources.
- Confirmed that all source control pages will inherit the same background, viewport size, and no-scroll/overflow policy for visual and functional consistency.
- Bumped project version to 0.7.0 to mark the start of the source control implementation phase.

#### Rationale
- Provides a robust foundation for building out source-specific control pages with guaranteed layout and style consistency.
- Ensures that all new sources can be added with minimal setup and will conform to project standards for touchpanel compatibility and user experience.
- The test PAC for PC (Source 1) allows rapid validation of routing and layout before building out full-featured source controls.

#### Technical Implementation
- Added `PCSourceTest.svelte` as a minimal, layout-compliant source control page.
- Imported and routed `PCSourceTest` for Source 1 in `SourceControlRouter.svelte` (controlType 1).
- Confirmed that all source control containers remain non-scrollable and fixed-size as per PRD and project memories.
- Ran a successful production build (`npm run build`) to verify all changes.

#### Files Changed
- `src/components/source-control/PCSourceTest.svelte`: new blank test PAC for Source 1 (PC)
- `src/components/source-control/SourceControlRouter.svelte`: import and routing update for Source 1
- `package.json`: version bumped to 0.7.0
- `Project Log.md`: release notes added for v0.7.0

---

## Version 0.7.1 — 2025-05-26

### Performance Optimization & Refactor Release

**Date:** 2025-05-26

#### Summary of Accomplishments
- **SourceControlRouter.svelte:**
  - Removed duplicate declaration of `controlTypeComponents` to resolve lint errors and ensure a single source of truth for component mapping.
  - Centralized and memoized the logic for determining which source control component to render, reducing unnecessary reactivity and improving performance.
  - Simplified the template to conditionally render only the enabled component, reducing DOM complexity.
- **App.svelte:**
  - Memoized derived state for `visibleSources`, `selectedSource`, and `visibleSourceIcons` to minimize unnecessary renders.
  - Fixed redeclaration issues for `selectedSource`.
- **General:**
  - Improved error handling and user feedback for unavailable or unknown control types.
  - Maintained strict adherence to runtime component configuration via Crestron signals, ensuring only enabled components are rendered.
  - Confirmed all optimizations are compatible with single-file builds and Crestron deployment requirements.

#### Rationale
- These changes significantly improve runtime performance by reducing unnecessary Svelte reactivity and DOM updates.
- Codebase is now easier to maintain, with clearer logic for component rendering and configuration.
- Lint errors are resolved, ensuring a cleaner development and build process.

#### Technical Implementation
- Refactored `SourceControlRouter.svelte` to have a single, authoritative `controlTypeComponents` mapping.
- Consolidated render logic into a single reactive statement for clarity and efficiency.
- Updated `App.svelte` for better state management and to eliminate redundant declarations.
- Ran successful production builds and CH5 archives to validate all changes.

#### Files Changed
- `src/components/source-control/SourceControlRouter.svelte`: render logic refactor, duplicate variable removal
- `src/App.svelte`: memoization, lint fixes
- `Project Log.md`: release notes for v0.7.1
- `CHANGELOG.md`: changelog entry for v0.7.1

### Changelog (see CHANGELOG.md for details)
- Refactored source control routing logic for performance and maintainability
- Removed duplicate variables, resolved all known lint errors
- Improved memoization of derived state in main app logic
- Enhanced error handling for unknown/unavailable control types
- Build and archive confirmed clean and deployable

---

For future releases, see this log for versioned summaries and major project milestones.
