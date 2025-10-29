import type { Metadata } from 'next';
import PropertyListPage from '@/modules/properties/pages/PropertyList';

export async function generateMetadata({ params }: any): Promise<Metadata> {

  return {
    title: "Properties List",
    icons: {
      icon: '/favicon.ico',
    }
  };
};

export default function PropertyList(props: any) {
  return <PropertyListPage {...props} />;
};
