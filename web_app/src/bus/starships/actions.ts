// Types
import {
  ErrorHttpAction,
  Starships,
  STARSHIPS_ERROR,
  STARSHIPS_FETCH_ASYNC,
  STARSHIPS_FILL,
  STARSHIPS_FINISH,
  STARSHIPS_START,
  StarshipsActionTypes,
  StarshipsErrorAction,
  StarshipsFillAction,
} from './types';

// Sync
export function start(): StarshipsActionTypes {
  return {
    type: STARSHIPS_START,
  };
}

export function finish(): StarshipsActionTypes {
  return {
    type: STARSHIPS_FINISH,
  };
}

export function fill(payload: Starships): StarshipsFillAction {
  return {
    type: STARSHIPS_FILL,
    payload,
  };
}

export function error(payload: ErrorHttpAction): StarshipsErrorAction {
  return {
    type: STARSHIPS_ERROR,
    error: true,
    payload,
  };
}

// Async
export function fetchAsync(): StarshipsActionTypes {
  return {
    type: STARSHIPS_FETCH_ASYNC,
  };
}
