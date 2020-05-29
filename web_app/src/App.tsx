// Core
import React, { ReactElement } from 'react';
import { Provider } from 'react-redux';

// Other
import { store } from './init/store';

// Domains
import { Starships } from './bus/starships';

export function App(): ReactElement {
  return (
    <Provider store={store}>
      <div className="App">
        <Starships />
      </div>
    </Provider>

  );
}
