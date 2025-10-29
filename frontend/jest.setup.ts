import "@testing-library/jest-dom";

// Required for React Router DOM
if(typeof globalThis.TextEncoder === 'undefined') {
    // eslint-disable-next-line @typescript-eslint/no-require-imports
    const { TextEncoder, TextDecoder }= require('util').TextEncoder;
    globalThis.TextEncoder = TextEncoder;
    globalThis.TextDecoder = TextDecoder;
}

// Supress console warnings during tests
const originalError = console.error;
beforeAll(() => {
    console.error = (...args: unknown[]) => {
        if (typeof args[0] === 'string' && args[0].includes('Warning')) {
            return;
        }
        originalError.call(console, ...args);
    }
});

afterAll(() => {
    console.error = originalError;
});