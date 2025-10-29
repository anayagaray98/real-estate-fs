import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { publicEnv } from "@/config/env";

const publicApi = axios.create({
  baseURL: publicEnv.NEXT_PUBLIC_API_URL,
  headers: { Accept: "application/json" },
  withCredentials: true,
});

export type RequestOptions = {
  isPublic?: boolean;
  locale?: string;
  cookies?: string;
  responseType?: string;
};

async function request<T = any>(
  config: AxiosRequestConfig,
  { isPublic = true, locale = "es", cookies, responseType }: RequestOptions = {}
): Promise<AxiosResponse<T>> {

  config.headers = {
    ...config.headers,
  };

  if (cookies) {
    config.headers["Cookie"] = cookies;
  }

  // Handle locale
  if (locale !== "es") {
    // Ensure leading slash on URL
    if (config.url && !config.url.startsWith("/en/")) {
      config.url = `/${locale}${config.url.startsWith("/") ? config.url : `/${config.url}`}`;
    }

    if (locale !== 'es') {
      config.params = {
        ...config.params,
        lang: locale,
      };
    }
  }

  if (responseType) {
    config.responseType = responseType as any;
  }

  return publicApi.request<T>(config);
};

// helpers
request.get = <T = any>(url: string, config?: AxiosRequestConfig, options?: RequestOptions) =>
  request<T>({ ...config, url, method: "GET" }, options);

request.post = <T = any>(url: string, data?: any, config?: AxiosRequestConfig, options?: RequestOptions) =>
  request<T>({ ...config, url, method: "POST", data }, options);

request.put = <T = any>(url: string, data?: any, config?: AxiosRequestConfig, options?: RequestOptions) =>
  request<T>({ ...config, url, method: "PUT", data }, options);

request.patch = <T = any>(url: string, data?: any, config?: AxiosRequestConfig, options?: RequestOptions) =>
  request<T>({ ...config, url, method: "PATCH", data }, options);

request.delete = <T = any>(url: string, config?: AxiosRequestConfig, options?: RequestOptions) =>
  request<T>({ ...config, url, method: "DELETE" }, options);

export default request;
