import { render, screen, act } from '@testing-library/react';
import { Provider } from 'react-redux';
import configureMockStore from 'redux-mock-store';
import PropertyListPage from '../pages/PropertyList';

const noopThunk = () => (next: any) => (action: any) => next(action);
const middlewares = [noopThunk];
const mockStore = configureMockStore(middlewares);

jest.mock('../store/actions', () => ({
  getProperties: jest.fn(() => Promise.resolve({ type: 'GET_PROPERTIES' })),
}));

jest.mock('../components/PropertyCard', () => ({
  PropertyCard: ({ property }: any) => <div data-testid="property-card">{property.name}</div>
}));

jest.mock('@/redux/hooks', () => ({
  useAppDispatch: () => () => Promise.resolve({ type: 'GET_PROPERTIES' }),
}));

describe('PropertyListPage', () => {
  let store: any;

  beforeEach(() => {
    store = mockStore({
      PropertyReducer: {
        properties: [
          { idProperty: '1', idOwner: '1', name: 'House 1', address: '123 St', price: 100000, year: 2020, codeInternal: 'RE001', image: '' },
          { idProperty: '2', idOwner: '2', name: 'House 2', address: '456 St', price: 200000, year: 2021, codeInternal: 'RE002', image: '' },
        ],
      },
    });
  });

  it('renders page title and description', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <PropertyListPage />
        </Provider>
      );
    });

    expect(screen.getByText(/Explore Our Properties/i)).toBeInTheDocument();
    expect(screen.getByText(/Browse through our latest listings/i)).toBeInTheDocument();
  });

  it('shows the number of properties', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <PropertyListPage />
        </Provider>
      );
    });

    expect(await screen.findByText(/2 properties available/i)).toBeInTheDocument();
  });

  it('renders property cards', async () => {
    await act(async () => {
      render(
        <Provider store={store}>
          <PropertyListPage />
        </Provider>
      );
    });

    // Wait for the property cards to appear
    const cards = await screen.findAllByTestId('property-card');
    expect(cards.length).toBe(2);
    expect(cards[0]).toHaveTextContent('House 1');
    expect(cards[1]).toHaveTextContent('House 2');
  });
});
