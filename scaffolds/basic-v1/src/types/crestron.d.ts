/**
 * Crestron Communication Library (CrComLib) type definitions
 */
declare global {
  interface Window {
    CrComLib?: CrComLibType;
  }

  interface CrComLibType {
    /**
     * Publishes an event/signal to the Crestron control system
     * @param type Signal type ('n' for number, 'b' for boolean, 's' for string)
     * @param signalName Named signal reference from contract
     * @param value Value to send
     */
    publishEvent(type: 'n', signalName: string, value: number): void;
    publishEvent(type: 'b', signalName: string, value: boolean): void;
    publishEvent(type: 's', signalName: string, value: string): void;

    /**
     * Subscribes to state changes from the Crestron control system
     * @param type Signal type ('n' for number, 'b' for boolean, 's' for string)
     * @param signalName Named signal reference from contract
     * @param callback Function called when signal changes
     * @returns Subscription ID for unsubscribing
     */
    subscribeState(type: 'n', signalName: string, callback: (value: number) => void): string;
    subscribeState(type: 'b', signalName: string, callback: (value: boolean) => void): string;
    subscribeState(type: 's', signalName: string, callback: (value: string) => void): string;

    /**
     * Unsubscribes from a signal
     * @param type Signal type
     * @param signalName Signal name
     * @param subscriptionId ID returned from subscribeState
     */
    unsubscribeState(type: string, signalName: string, subscriptionId: string): void;

    /**
     * Gets the current value of a signal
     * @param type Signal type
     * @param signalName Signal name
     * @returns Current signal value
     */
    getState(type: 'n', signalName: string): number | undefined;
    getState(type: 'b', signalName: string): boolean | undefined;
    getState(type: 's', signalName: string): string | undefined;
  }

  // Make CrComLib available globally
  const CrComLib: CrComLibType | undefined;
}

export {};