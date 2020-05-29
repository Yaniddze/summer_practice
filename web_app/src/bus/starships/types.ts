export type Starship = {
  name: string;
};

export type Starships = {
  results: Starship[];
};

export type ErrorHttpAction = string;

// Sync
export const STARSHIPS_START = 'STARSHIPS_START';
type StarshipsStartAction = {
  type: typeof STARSHIPS_START;
};

export const STARSHIPS_FINISH = 'STARSHIPS_FINISH';
type StarshipsFinishAction = {
  type: typeof STARSHIPS_FINISH;
};

export const STARSHIPS_FILL = 'STARSHIPS_FILL';
export type StarshipsFillAction = {
  type: typeof STARSHIPS_FILL;
  payload: Starships;
};

export const STARSHIPS_ERROR = 'STARSHIPS_ERROR';
export type StarshipsErrorAction = {
  type: typeof STARSHIPS_ERROR;
  error: true;
  payload: ErrorHttpAction;
};

// Async
export const STARSHIPS_FETCH_ASYNC = 'STARSHIPS_FETCH_ASYNC';
type StarshipsFetchAsyncAction = {
  type: typeof STARSHIPS_FETCH_ASYNC;
};

export type StarshipsActionTypes =
    | StarshipsStartAction
    | StarshipsFinishAction
    | StarshipsFillAction
    | StarshipsErrorAction
    | StarshipsFetchAsyncAction;
