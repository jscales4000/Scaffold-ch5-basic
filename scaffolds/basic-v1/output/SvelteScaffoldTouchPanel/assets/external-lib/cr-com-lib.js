/**
 * CrComLib Mock for Development
 * This is a development stub that provides the basic CrComLib API
 * In production, this will be replaced by the actual Crestron CrComLib
 */

window.CrComLib = {
    // Mock for publishEvent - logs to console in development
    publishEvent: function(type, join, value) {
        console.log(`[CrComLib] Publishing ${type}:${join} = ${value}`);
        
        // In development, we can simulate some responses
        if (type === 'd' && join === '1.3' && value === true) {
            // Simulate mute feedback
            setTimeout(() => {
                if (this.subscriptions && this.subscriptions['d:2.1']) {
                    this.subscriptions['d:2.1'](true);
                }
            }, 50);
        }
        
        if (type === 'a' && join === '1.1') {
            // Simulate source selection feedback
            setTimeout(() => {
                if (this.subscriptions && this.subscriptions['a:2.1']) {
                    this.subscriptions['a:2.1'](value);
                }
            }, 50);
        }
    },
    
    // Mock for subscribeState - stores callbacks for simulation
    subscribeState: function(type, join, callback) {
        console.log(`[CrComLib] Subscribing to ${type}:${join}`);
        
        if (!this.subscriptions) {
            this.subscriptions = {};
        }
        
        this.subscriptions[`${type}:${join}`] = callback;
        
        // Simulate initial values
        if (type === 'd' && join === '2.1') {
            // Initial mute state
            setTimeout(() => callback(false), 100);
        } else if (type === 'd' && join === '2.2') {
            // Initial power state
            setTimeout(() => callback(true), 100);
        } else if (type === 'a' && join === '2.1') {
            // Initial source
            setTimeout(() => callback(0), 100);
        } else if (type === 'a' && join === '2.2') {
            // Initial volume (50% = 32767)
            setTimeout(() => callback(32767), 100);
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