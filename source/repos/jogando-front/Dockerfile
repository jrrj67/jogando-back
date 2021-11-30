FROM node:12
ENV CHOKIDAR_USEPOLLING=true
COPY package.json /app/package.json
WORKDIR /app
RUN npm install @vue/cli -g
RUN npm install