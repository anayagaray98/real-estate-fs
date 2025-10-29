// Public variables (available to client)
export const publicEnv = {
  NEXT_PUBLIC_API_URL: process.env.NEXT_PUBLIC_API_URL as string || "http://localhost:8080",
};