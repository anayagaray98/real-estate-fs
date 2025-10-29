'use client';

import { connect } from 'react-redux';
import React, { useEffect, useState } from "react";
import { useAppDispatch } from '@/redux/hooks';

import { PropertyCard } from "../components/PropertyCard";
import { getProperties } from '../store/actions';

import type { RootState } from '@/redux/store';
import type { PropertyListPageProps } from '../types/page.interfaces';

const PropertyListPage: React.FC<PropertyListPageProps> = ({ properties }) => {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        dispatch(getProperties())
            .finally(() => setLoading(false));
    }, [dispatch]);

    return (
        <div className="container mx-auto px-4 py-8">
            <div className="mb-8 text-center">
                <h1 className="text-3xl font-bold text-gray-800">Explore Our Properties</h1>
                <p className="text-gray-500 mt-2">
                    Browse through our latest listings and find your dream home.
                </p>
                {properties?.length && (
                    <p className="text-gray-400 mt-1 text-sm">
                        {properties.length} properties available
                    </p>
                )}
            </div>

            {loading ? (
                <div className="flex justify-center items-center h-64">
                    <span className="text-gray-500">Loading properties...</span>
                </div>
            ) : (
                <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                    {properties?.map((property) => (
                        <PropertyCard key={property.idProperty} property={property} />
                    ))}
                </div>
            )}
        </div>
    );
};

const mapStateToProps = (state: RootState) => ({
    properties: state.PropertyReducer.properties,
});

export default connect(mapStateToProps)(PropertyListPage);
