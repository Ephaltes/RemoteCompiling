FROM node:alpine  AS website-build
WORKDIR /app
COPY . .
RUN npm ci && npm run build


FROM nginx:alpine
COPY --from=website-build /app/dist/trydotnetandjavaandpython /usr/share/nginx/html
EXPOSE 80