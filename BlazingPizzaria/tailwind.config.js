/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{html,js,razor}"],
    theme: {
     
        fontFamily: {
            "sans": ['Poppins', 'sans-serif']
        },
      extend: {
          backgroundImage: {
              "home": "url('/img/Layout/background.jpg')"
          }

      },
  },
  plugins: [],
}

