import { ListProperty, DetailProperty } from "./object.interfaces";

export interface PropertyListPageProps {
    properties: ListProperty[] | null
};

export interface PropertyDetailPageProps {
    property: DetailProperty | null;
    relatedProperties: ListProperty[] | null;
};