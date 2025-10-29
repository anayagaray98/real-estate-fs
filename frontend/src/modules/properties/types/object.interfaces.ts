export interface ListProperty {
    idProperty: string;
    idOwner: string;
    name: string;
    address: string;
    price: number;
    year: number;
    codeInternal: string;
    image: string;
};

export interface PropertyImage {
    idPropertyImage: string;
    file: string;
    enabled: boolean;
}

export interface DetailProperty {
    idProperty: string;
    idOwner: string;
    name: string;
    address: string;
    price: number;
    year: number;
    codeInternal: string;
    images: PropertyImage[];
    traces: string[];
};