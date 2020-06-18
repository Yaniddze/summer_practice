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
} from './types';

export type StarshipState = {
  data: Starships;
  isFetching: boolean;
  error: false | ErrorHttpAction;
};

const initialState: StarshipState = {
  data: {
    results: [],
  },
  isFetching: false,
  error: false,
};

export const starshipsReducer = (
  state = initialState,
  action: StarshipsActionTypes,
): StarshipState => {
  switch (action.type) {
    case STARSHIPS_START:
      return {
        ...state,
        error: false,
        isFetching: true,
      };

    case STARSHIPS_FINISH:
      return {
        ...state,
        error: false,
        isFetching: false,
      };

    case STARSHIPS_ERROR:
      return {
        ...state,
        isFetching: false,
        error: action.payload,
      };

    case STARSHIPS_FILL:
      return {
        ...state,
        error: false,
        isFetching: false,
        data: {
          ...action.payload,
        },
      };

    case STARSHIPS_FETCH_ASYNC:
      return state;

    default:
      // eslint-disable-next-line no-case-declarations,@typescript-eslint/no-unused-vars
      const x: never = action;
  }
  return state;
};
