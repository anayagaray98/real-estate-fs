import request from "@/shared/api/client";
import type { ListProperty, DetailProperty } from "../types/object.interfaces";


export const apiGetProperties = (
    params: Record<string, unknown>,
) => 
    request.get<ListProperty>("/api/properties/", { params }, {});

export const apiGetDetailProperty = (id: string) => 
    request.get<DetailProperty>(`/api/properties/${id}`);