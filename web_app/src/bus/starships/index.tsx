import React, { ReactElement, FC } from 'react';
import { useStarshipsFetch } from './hooks/useStarshipsFetch';
import { Title } from '../../elements/title';
import { Starship } from './types';

export const Starships: FC = () => {
  const { isFetching, data, error } = useStarshipsFetch();

  const errorMessageJSX = error && <p>Not found!</p>;
  const loaderJSX = isFetching && <p>Loading data from API...</p>;
  const listJSX = isFetching
      || data.results?.map(
          ({ name }: Starship, index: number): ReactElement => <li key={Number(index)}>{name}</li>,
      );

  return (
    <>
      <Title>Starships</Title>
      {errorMessageJSX}
      {loaderJSX}
      <ul>{listJSX}</ul>
    </>
  );
};
