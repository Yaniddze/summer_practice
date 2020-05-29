import { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';

import { fetchAsync } from '../actions';
import { StarshipState } from '../reducer';
import { AppState } from '../../../init/rootReducer';

export const useStarshipsFetch = (): StarshipState => {
  const dispatch = useDispatch();
  const { data, isFetching, error } = useSelector<AppState, StarshipState>(
    (state) => state.starships,
  );

  useEffect(() => {
    dispatch(fetchAsync());
  }, [dispatch]);

  return {
    data,
    isFetching,
    error,
  };
};
