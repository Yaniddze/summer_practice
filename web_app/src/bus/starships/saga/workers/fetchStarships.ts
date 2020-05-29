// Tools
import {
  start, finish, fill, error,
} from '../../actions';
import { Starships } from '../../types';

// Workers
import { makeRequestWithSpinner } from '../../../../workers';

// API
import { api } from '../../../../api';

export function* fetchStarships(): Generator {
  const options = {
    fetcher: api.starships.fetch,
    start,
    finish,
    fill,
    error,
  };

  yield makeRequestWithSpinner<Starships>(options);
}
