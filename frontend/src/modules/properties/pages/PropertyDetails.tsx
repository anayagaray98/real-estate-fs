'use client';

import { connect } from 'react-redux';
import React, { useState } from "react";
import { PropertyCard } from "../components/PropertyCard";

import Image from 'next/image';

import type { RootState } from '@/redux/store';
import type { PropertyDetailPageProps } from '../types/page.interfaces';
import type { PropertyImage } from '../types/object.interfaces';


const PropertyDetailPage: React.FC<PropertyDetailPageProps> = ({ property, relatedProperties }) => {
    const [selectedImage, setSelectedImage] = useState<PropertyImage | null>(property?.images?.[0] ?? null);

    if (!property) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                <span className="text-gray-500">Property not found.</span>
            </div>
        );
    }

    return (
        <div className="container mx-auto px-4 py-8">
            <div className="mb-8 text-center">
                <h1 className="text-4xl font-bold text-gray-800">{property.name}</h1>
                <p className="text-gray-500 mt-2">{property.address}</p>
                <p className="text-gray-700 font-semibold mt-1 text-lg">
                    ${property.price.toLocaleString()} &bull; Year: {property.year} &bull; Code: {property.codeInternal}
                </p>
            </div>
            {property.images?.length > 0 && (
                <div className="flex flex-col md:flex-row gap-4 mb-8">
                    <div className="flex-1 h-96">
                        {selectedImage && (
                            <Image
                                src={selectedImage.file}
                                alt={property.name}
                                className="w-full h-full object-cover rounded-lg shadow-lg"
                            />
                        )}
                    </div>
                    <div className="flex md:flex-col gap-2 overflow-x-auto md:overflow-x-hidden md:w-32">
                        {property.images.map((img, idx) => (
                            <Image
                                key={idx}
                                src={img.file}
                                alt={`Property Image ${idx + 1}`}
                                className={`w-20 h-20 object-cover rounded-lg cursor-pointer border-2 ${
                                    selectedImage === img ? 'border-red-600' : 'border-transparent'
                                }`}
                                onClick={() => setSelectedImage(img)}
                            />
                        ))}
                    </div>
                </div>
            )}
            <div className="mb-12 bg-gray-50 p-6 rounded-lg shadow">
                <h2 className="text-2xl font-semibold mb-4">Property Details</h2>
                <ul className="space-y-2 text-gray-700">
                    <li><strong>Address:</strong> {property.address}</li>
                    <li><strong>Price:</strong> ${property.price.toLocaleString()}</li>
                    <li><strong>Year Built:</strong> {property.year}</li>
                    <li><strong>Internal Code:</strong> {property.codeInternal}</li>
                    <li><strong>Owner ID:</strong> {property.idOwner}</li>
                </ul>
            </div>
            {relatedProperties && relatedProperties?.length > 0 && (
                <div>
                    <h2 className="text-center text-4xl font-semibold mb-6">Related Properties</h2>
                    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                        {relatedProperties.map((prop) => (
                            <PropertyCard key={prop.idProperty} property={prop} />
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

const mapStateToProps = (state: RootState) => ({
    relatedProperties: state.PropertyReducer.properties
});

export default connect(mapStateToProps)(PropertyDetailPage);
