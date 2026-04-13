# Asisste.Web

Portal web moderno para reemplazar progresivamente el MVC legado.

## Stack base

- React
- TypeScript
- Vite

## Slice implementado

- Catálogo de perfiles consumiendo `GET/POST/PUT/DELETE /api/profiles`
- Reemplazo inicial de `Views/Maestros/Perfil.cshtml`
- Reemplazo de `js/Perfil/Perfil.js` mediante hook y servicio HTTP tipado

## Arranque local

1. `npm install`
2. `npm run dev`

Opcional:

- definir `VITE_API_BASE_URL` si la API no corre en `http://localhost:5075`