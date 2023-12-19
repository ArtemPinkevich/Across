FROM node:16.15.1-alpine

WORKDIR /app

COPY package.json /app

RUN npm install --legacy-peer-deps

COPY . /app

ENV SERVER_API_URL="http://localhost:5000"

EXPOSE 3000

CMD ["npm", "start"]