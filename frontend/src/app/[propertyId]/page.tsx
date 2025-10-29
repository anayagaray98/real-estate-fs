import type { Metadata } from 'next';
import PropertyDetailPage from '@/modules/properties/pages/PropertyDetails';
import { apiGetDetailProperty } from '@/modules/properties/api';
import { cache } from "react";


/**
 * Cached property fetcher — avoids multiple backend requests
 */
const preloadProperty = cache(async (propertyId: string) => {
  const res = await apiGetDetailProperty(propertyId);
  return res.data;
});


export async function generateMetadata({ params }: any): Promise<Metadata> {

    const { propertyId } = await params;
    const propertyData = await preloadProperty(propertyId);

  return {
    title: propertyData.name,
    icons: {
      icon: '/favicon.ico',
    }
  };
};

export default async function PropertyDetail(props: any) {
    const { propertyId } = await props.params;
    const propertyData = await preloadProperty(propertyId);
    return <PropertyDetailPage property={propertyData} {...props} />;
};
