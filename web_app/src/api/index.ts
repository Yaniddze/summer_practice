import axios from 'axios';
import { root } from './config';
import { Starships } from '../bus/starships/types';

export type FetchDataType<T> = () => Promise<T>;

type APIFetchDataType = {
  starships: {
    fetch: FetchDataType<Starships>;
  };
};

export const api: APIFetchDataType = {
  starships: {
    fetch: (): Promise<Starships> => axios.get(`${root}/weatherforecast`).then((result) => {
      const data = result.data.map((starship: any) => ({ name: starship.summary }));
      return { results: data };
    }),
  },
};
