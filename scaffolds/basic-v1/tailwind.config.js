module.exports = {
  content: [
    './src/**/*.{html,js,svelte,ts}',
    './public/index.html'
  ],
  theme: {
    extend: {
      colors: {
        primary: '#0C234B',
        secondary: '#AB0520',
        midnight: '#12263A',
        white: '#FFFFFF',
        warmGray: '#F4EDE5',
        coolGray: '#E2E9EB',
        // Extend with more as needed
      }
    }
  },
  plugins: []
};
