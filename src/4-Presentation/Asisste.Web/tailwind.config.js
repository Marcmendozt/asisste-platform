/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{ts,tsx}"],
  theme: {
    extend: {
      colors: {
        asisste: {
          50: "#f2f8fc",
          100: "#e3eff7",
          200: "#c5deed",
          300: "#9ec6df",
          400: "#6ea6ca",
          500: "#4e91c2",
          600: "#2f73a7",
          700: "#1f5b86",
          800: "#173f5e",
          900: "#132d44"
        },
        brandgold: {
          100: "#fff2d8",
          300: "#f4cf82",
          500: "#d99517",
          700: "#8d5c00"
        }
      },
      fontFamily: {
        sans: ["Bahnschrift", "Trebuchet MS", "Segoe UI Variable", "Segoe UI", "sans-serif"],
        display: ["Franklin Gothic Medium", "Bahnschrift", "Trebuchet MS", "sans-serif"]
      }
    }
  },
  plugins: []
};

