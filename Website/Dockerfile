FROM node:16-alpine

RUN echo "fs.inotify.max_user_instances=524288" >> /etc/sysctl.conf && sysctl -p

WORKDIR /app

COPY package.json /app

RUN npm install --force

COPY . /app

ENV SERVER_API_URL="http://localhost:5000"

EXPOSE 3000

CMD ["npm", "start"]