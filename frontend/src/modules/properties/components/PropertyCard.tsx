import Link from "next/link";
import Image from "next/image";
import type { PropertyCardProps } from "../types/component.interfaces";

export const PropertyCard = ({ property }: PropertyCardProps) => {
  return (
    <Link href={`/${property.idProperty}`} passHref>
      <div className="cursor-pointer bg-white rounded-xl shadow-lg overflow-hidden hover:shadow-2xl transition-shadow duration-300 w-full max-w-sm mx-auto">
        {/* Property Image */}
        <div className="h-60 w-full overflow-hidden">
          <Image
            src={property.image}
            alt={property.name}
            className="w-full h-full object-cover transition-transform duration-500 hover:scale-105"
          />
        </div>

        {/* Card Content */}
        <div className="p-5">
          <h2 className="text-xl font-semibold text-gray-800 truncate">{property.name}</h2>
          <p className="text-gray-500 mt-1 truncate">{property.address}</p>

          <div className="mt-3 flex flex-wrap gap-2 text-sm text-gray-600">
            <span className="bg-gray-100 px-2 py-1 rounded-full">Price: ${property.price.toLocaleString()}</span>
            <span className="bg-gray-100 px-2 py-1 rounded-full">Year: {property.year || "N/A"}</span>
            <span className="bg-gray-100 px-2 py-1 rounded-full">Code: {property.codeInternal}</span>
          </div>
        </div>
      </div>
    </Link>
  );
};
