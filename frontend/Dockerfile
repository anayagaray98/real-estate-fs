# Stage 1: Dependencies
FROM node:24.4.1-alpine AS deps

WORKDIR /frontend

COPY package.json package-lock.json* ./

RUN npm install -g npm@11.4.2
RUN npm install -g serve

# Clean install from package-lock.json for reproducible builds
RUN npm ci

# Stage 2: Builder
FROM node:24.4.1-alpine AS builder

WORKDIR /frontend

COPY . .
COPY --from=deps /frontend/node_modules ./node_modules


# Disable Next.js anonymous telemetry in builds and runtime
ENV NEXT_TELEMETRY_DISABLED=1

RUN npm run build


# Stage 3: Runner
FROM node:24.4.1-alpine AS runner

# Set Node.js environment to production for optimized behavior
ENV NODE_ENV=production

# Create a non-root user for security
RUN addgroup --system --gid 1001 nextjs
RUN adduser --system --uid 1001 nextjs

WORKDIR /frontend

COPY --from=builder /frontend/.next ./.next
COPY --from=builder /frontend/node_modules ./node_modules
COPY --from=builder /frontend/package.json ./package.json
COPY --from=builder /frontend/public ./public
COPY --from=builder /frontend/next.config.js ./next.config.js
COPY --from=builder /frontend/jsconfig.json ./jsconfig.json 
COPY --from=builder /frontend/next-i18next.config.js ./next-i18next.config.js

RUN chown -R nextjs:nextjs /frontend/.next /frontend/node_modules /frontend/public

USER nextjs

CMD ["npm", "start"]

EXPOSE 3000




