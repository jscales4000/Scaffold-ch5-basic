# Source Control Router: Signal-to-Page Mapping and Subpage Triggering

## Analog Signal Mapping for Source Selection

The following analog signals from the contract file control which source control subpage/component is visible in the UI:

| Analog Join | Signal Name                              | Purpose                                  |
|-------------|------------------------------------------|------------------------------------------|
| 4.37        | Settings.SourceCntrlIDfromcntrlSys1      | Source Control ID from Control System 1   |
| 4.38        | Settings.SourceCntrlIDfromcntrlSys2      | Source Control ID from Control System 2   |
| 4.39        | Settings.SourceCntrlIDfromcntrlSys3      | Source Control ID from Control System 3   |
| 4.40        | Settings.SourceCntrlIDfromcntrlSys4      | Source Control ID from Control System 4   |
| 4.41        | Settings.SourceCntrlIDfromcntrlSys5      | Source Control ID from Control System 5   |
| 4.42        | Settings.SourceCntrlIDfromcntrlSys6      | Source Control ID from Control System 6   |
| 4.43        | Settings.SourceCntrlIDfromcntrlSys7      | Source Control ID from Control System 7   |
| 4.44        | Settings.SourceCntrlIDfromcntrlSys8      | Source Control ID from Control System 8   |

Each signal holds a numeric value representing the currently selected source for that control system. The mapping of values to source pages is as follows (based on the current Svelte codebase):

| Value | Svelte Component                | Source Name (Typical)           |
|-------|----------------------------------|---------------------------------|
| 1     | TextSource                       | Text Source                     |
| 2     | MediaPlayerSource                | Media Player                    |
| 3     | DocumentCameraSource             | Document Camera                 |
| 4     | AirmediaSource                   | AirMedia                        |
| 5     | LectureCaptureSource             | Lecture Capture                 |
| 6     | CameraControlSource              | Camera Control                  |
| 7     | LaptopSource                     | Laptop                          |
| 8     | RoomPCSource                     | Room PC                         |
| 9     | InteractiveWhiteboardSource      | Interactive Whiteboard          |
| 10    | VideoConferenceSource            | Video Conference                |

## How Subpage Visibility is Triggered

- The `SourceControlRouter.svelte` component receives a `controlTypeSignalName` prop (e.g., `Settings.SourceCntrlIDfromcntrlSys1`).
- On mount and whenever the signal name changes, it subscribes to the specified analog signal using `CrComLib.subscribeState`.
- When the signal value changes, the local `controlType` variable is updated.
- A mapping table (`controlTypeComponents`) maps each analog value to a Svelte component.
- The markup uses `<svelte:component this={currentComponent} ... />` to render the correct subpage/component.
- If the value is not mapped, an error or fallback message is shown.

## Summary
- Changing the value of the analog signal (e.g., `Settings.SourceCntrlIDfromcntrlSys1`) from the control processor or backend will immediately trigger the UI to switch to the corresponding source control subpage.
- The system is fully dynamic and can be extended by updating the mapping table and adding new components as needed.

---

_Last updated: 2025-05-23_
