/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{html,js,razor}"],
  theme: {
      extend: {
          backgroundImage: {
              "home": "url('/img/Layout/background.jpg')"
          }
      },
  },
  plugins: [],
}

