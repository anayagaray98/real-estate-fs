import { render, screen } from '@testing-library/react';
import { PropertyCard } from './PropertyCard';

const mockProperty = {
  idOwner: '1',
  idProperty: '1',
  name: 'Beautiful House',
  address: '123 Main St',
  price: 720000000,
  year: 2025,
  codeInternal: 'RE0001',
  image: 'https://example.com/image.jpg'
};


// Mock next/image to a simple img tag for tests
jest.mock('next/image', () => ({
  __esModule: true,
  default: (props: any) => {
    // eslint-disable-next-line @next/next/no-img-element
    return <img {...props} />;
  },
}));

describe('PropertyCard', () => {
  it('renders property information correctly', () => {
    render(<PropertyCard property={mockProperty} />);
    expect(screen.getByText('Beautiful House')).toBeInTheDocument();
    expect(screen.getByText('123 Main St')).toBeInTheDocument();
    expect(
      screen.getByText((content) => content.includes('Price: $720'))
    ).toBeInTheDocument();
    expect(screen.getByText('Year: 2025')).toBeInTheDocument();
    expect(screen.getByText('Code: RE0001')).toBeInTheDocument();
  });
});
