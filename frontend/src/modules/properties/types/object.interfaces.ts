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

export interface DetailProperty {
    idProperty: string;
    idOwner: string;
    name: string;
    address: string;
    price: number;
    year: number;
    codeInternal: string;
    images: string[];
    traces: string[];
};