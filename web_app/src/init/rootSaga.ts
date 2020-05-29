// Core
import { all, fork } from 'redux-saga/effects';

// Watchers
import { watchStarships } from '../bus/starships/saga';

export function* rootSaga(): Generator {
  yield all([fork(watchStarships)]);
}
