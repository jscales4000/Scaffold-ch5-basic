/**
 * CrComLib Mock for Development
 * This is a development stub that provides the basic CrComLib API
 * In production, this will be replaced by the actual Crestron CrComLib
 */

window.CrComLib = {
    // Mock for publishEvent - logs to console in development
    publishEvent: function(type, signalName, value) {
        console.log(`[CrComLib] Publishing ${type}:${signalName} = ${value}`);
        
        // In development, we can simulate some responses
        if (type === 'b' && signalName === 'MuteToggle' && value === true) {
            // Simulate mute feedback
            setTimeout(() => {
                if (this.subscriptions && this.subscriptions['b:IsMuted']) {
                    this.subscriptions['b:IsMuted'](true);
                }
            }, 50);
        }
        
        if (type === 'n' && signalName === 'SelectSource') {
            // Simulate source selection feedback
            setTimeout(() => {
                if (this.subscriptions && this.subscriptions['n:SelectedSource']) {
                    this.subscriptions['n:SelectedSource'](value);
                }
            }, 50);
        }
    },
    
    // Mock for subscribeState - stores callbacks for simulation
    subscribeState: function(type, signalName, callback) {
        console.log(`[CrComLib] Subscribing to ${type}:${signalName}`);
        
        if (!this.subscriptions) {
            this.subscriptions = {};
        }
        
        this.subscriptions[`${type}:${signalName}`] = callback;
        
        // Simulate initial values
        if (type === 'b' && signalName === 'IsMuted') {
            // Initial mute state
            setTimeout(() => callback(false), 100);
        } else if (type === 'b' && signalName === 'SystemPowered') {
            // Initial power state
            setTimeout(() => callback(true), 100);
        } else if (type === 'n' && signalName === 'SelectedSource') {
            // Initial source
            setTimeout(() => callback(0), 100);
        }
    },
    
    // Storage for subscriptions
    subscriptions: {},
    
    // Mock connection status
    isConnected: function() {
        return true;
    },
    
    // Version info
    version: "Development Mock v1.0"
};

console.log("[CrComLib] Development mock loaded successfully");
console.log("[CrComLib] Version:", window.CrComLib.version);