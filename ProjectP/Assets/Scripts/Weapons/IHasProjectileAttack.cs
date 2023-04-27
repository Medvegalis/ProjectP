using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHasProjectileAttack
{
	public void setProjectileAmount(int count);

	public void updateProjectileAmountFromAbility();

	public void setProjectileSpeed(float speed);

	public void updateProjectileSpeedFromAbility();
}

