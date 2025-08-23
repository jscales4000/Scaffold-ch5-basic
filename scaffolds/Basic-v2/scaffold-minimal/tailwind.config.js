/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{html,js,svelte,ts}'],
  theme: {
    screens: {
      'ts770': '1280px',    // TS-770 resolution
      'ts1070': '1920px',   // TS-1070 resolution
    },
    extend: {
      colors: {
        // University of Arizona colors
        'ua-blue': '#0C234B',
        'ua-blue-light': '#1E5288',
        'ua-red': '#AB0520',
        'ua-red-light': '#C41230'
      },
      spacing: {
        '128': '32rem',
        '144': '36rem',
      },
      fontSize: {
        'xxs': '0.8125rem',  // 13px at 16px base
        'base': '1rem',      // 16px base
        'lg': '1.25rem',     // 20px
        'xl': '1.5rem',      // 24px
        '2xl': '1.875rem',   // 30px
        '3xl': '2.25rem',    // 36px
        '4xl': '3rem',       // 48px
      },
      width: {
        'screen-ts770': '1280px',
      },
      height: {
        'screen-ts770': '800px',
      },
    }
  },
  plugins: [

  ]
}
