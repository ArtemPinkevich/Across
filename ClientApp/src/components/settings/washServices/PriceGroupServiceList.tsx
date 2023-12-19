import React from 'react';
import { Divider, Grid } from '@mui/material';
import { IPriceGroup } from '../../../store/PricePolicyStore';
import { PriceGroupServiceItem } from './PriceGroupServiceItem';
import { IPriceGroupService } from '../../../store/WashServices';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../../store';

export interface IPriceGroupServiceListProps {
  priceGroupServiceList: IPriceGroupService[];
}

export const PriceGroupServiceList: React.FC<IPriceGroupServiceListProps> = React.memo((props: IPriceGroupServiceListProps) => {
  const { priceGroupServiceList } = props;
  const priceGroupList: IPriceGroup[] = useSelector((state: ApplicationState) => state.pricePolicyState!.allPriceGroup);

  return (
    <Grid container direction='column'>
      {priceGroupList.map((priceGroup, key) => {
        return (
          <div key={key}>
            <Divider />
            <PriceGroupServiceItem
              priceGroup={priceGroup}
              priceGroupService={priceGroupServiceList.find((x) => x.priceGroupId === priceGroup.id) ?? ({ priceGroupId: priceGroup.id } as IPriceGroupService)}
            />
          </div>
        );
      })}
    </Grid>
  );
});
