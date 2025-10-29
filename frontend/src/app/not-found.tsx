'use client';

import React from 'react';
import Link from 'next/link';

interface NotFoundPageProps {
  title?: string;
  message?: string;
  showHomeLink?: boolean;
};

export const NotFoundPage: React.FC<NotFoundPageProps> = ({
  title = "Oops! Something went wrong",
  message = "We couldn't find the page you're looking for.",
  showHomeLink = true,
}) => {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gray-50 px-4">
      {/* Error Icon */}
      <div className="bg-red-100 rounded-full p-6 mb-6">
        <svg
          className="w-12 h-12 text-red-600"
          fill="none"
          stroke="currentColor"
          strokeWidth={2}
          viewBox="0 0 24 24"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M12 8v4m0 4h.01M21 12c0 4.97-4.03 9-9 9s-9-4.03-9-9 4.03-9 9-9 9 4.03 9 9z"
          />
        </svg>
      </div>

      {/* Error Text */}
      <h1 className="text-3xl font-bold text-gray-800 mb-2 text-center">{title}</h1>
      <p className="text-gray-500 mb-6 text-center">{message}</p>

      {/* Home Button */}
      {showHomeLink && (
        <Link
          href="/"
          className="bg-red-600 hover:bg-red-700 text-white font-semibold px-6 py-3 rounded-md transition-colors"
        >
          Go Home
        </Link>
      )}
    </div>
  );
};

export default NotFoundPage;
