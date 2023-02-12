using UnityEngine;
using System.Collections;


public class AIWeapon : Weapon {
    private GameObject target;
    private CharacterController targetCharacterController;
    [HideInInspector]
    public AIStateWeaponController aiWeaponController;


	 // Invoked from Fire method within Weapon.cs
	public  override bool Aim(out RaycastHit hitData){
        target = aiWeaponController.senses.target;
        targetCharacterController = target.GetComponent<CharacterController>();
		int numberBodyPartsVisible;
        // Which parts of the target's body are visible to the AI
        HealthManager.BodyParts[] visibleBodyParts = GetTargetVisibility(out numberBodyPartsVisible);
        if (numberBodyPartsVisible > 0) {
           // Debug.Log("Hit");
            // Pick a random visible player body part 
            HealthManager.BodyParts bpTarget = GetRandomVisibleBodyPart(visibleBodyParts, numberBodyPartsVisible);
            // Get info about the body part - damage & probability of hitting it
            TargetData targetData = GetTargetBodyPartData(bpTarget);
            return FireAtTarget(targetData, out hitData, out trajectory);
        }
        else {
           // Debug.Log("Missed");
            // If no visible body parts aim in the head direction in order to generate collision effects
            TargetData targetData = GetTargetBodyPartData(HealthManager.BodyParts.Head);
            return FireAtTarget(targetData, out hitData, out trajectory);
        }
	}
	


    // Invoked from Fire method within Weapon.cs
	public override bool InflictDamage(RaycastHit hitData){
        // Applying damage to target with valid tag
        if (hitData.collider.tag == Tags.Player || hitData.collider.tag == Tags.FriendlyAI || hitData.collider.tag == Tags.AI) {
            // Get the target's health manager
            HealthManager hm = aiWeaponController.senses.target.GetComponent<HealthManager>();
            // If target has health manager
			if (hm) {
                // Create a damage object
				DamageData damageData = new DamageData(gameObject,hitData.point,gameObject.transform.position,50);
                // Send it to the health manager's callback to be applied
				hm.OnRaycastHit(damageData);
				return true;
			}
			else return false;
		}
		else return false;
	}
	

    // Returns a list of target body parts that were visible to the AI
	protected HealthManager.BodyParts[] GetTargetVisibility(out int i){
        // Array to hold visible body parts
		HealthManager.BodyParts[] temp = new HealthManager.BodyParts[4];
		i = 0;
        float targetHeight = targetCharacterController.height;
		float yOffset;
		RaycastHit hitData;
		Vector3 direction;
        // For each of the target's body parts
		foreach(int bp in System.Enum.GetValues(typeof(HealthManager.BodyParts))){
            // Location of body part on target
			yOffset = (targetHeight * (bp/100.0f));
            // Direction from AI to target's body part
			direction = (new Vector3(target.transform.position.x,target.transform.position.y + yOffset,target.transform.position.z) - muzzlePoint.position).normalized;
            // Cast the ray
			bool hit = Physics.Raycast(muzzlePoint.position,direction,out hitData, aiWeaponController.senses.sightRange);
			Debug.DrawRay(muzzlePoint.position,direction * aiWeaponController.senses.sightRange,Color.yellow);
            // Did the ray hit something
			if (hit){
                // If it was a valid target
                if (hitData.collider.tag == Tags.Player || hitData.collider.tag == Tags.FriendlyAI || hitData.collider.tag == Tags.AI) {
                    // Add the body part to the array
					temp[i] = (HealthManager.BodyParts)bp;
					i++;
				}
			}
		}
		return temp;
	}

    // Pick a random visible target body part from the argument list of visible body parts
    protected HealthManager.BodyParts GetRandomVisibleBodyPart(HealthManager.BodyParts[] bp, int numberParts) {
        int randomIndex = Random.Range(0, numberParts);
        return bp[randomIndex];
    }
	
    // Get information about a particular body part hit, encapsulated within a TargetData object
	protected TargetData GetTargetBodyPartData(HealthManager.BodyParts bp){
       // Debug.Log("bp str : " + bp.ToString());
		TargetData hd = new TargetData();
        // Calculate hit location of body part
        float offset = targetCharacterController.height * ((int)bp / 100.0f);
		hd.location = new Vector3(target.transform.position.x,target.transform.position.y + offset,target.transform.position.z);
		hd.bodyPart = bp;
        // Determine potential damage to inflict and probability of hitting body part
		switch (bp) {
            case HealthManager.BodyParts.Legs: hd.damage = 50;
                hd.probability = HealthManager.Probability.Poor;
									           break;
            case HealthManager.BodyParts.Waiste: hd.damage = 30;
                                               hd.probability = HealthManager.Probability.Fair;
									          break;
            case HealthManager.BodyParts.Torso: hd.damage = 40;
                                              hd.probability = HealthManager.Probability.Excellent;
									          break;
            case HealthManager.BodyParts.Head: hd.damage = 15;
                                              hd.probability = HealthManager.Probability.Good;
									          break;
		}
		return hd;
	}
	

	

    // Fire the ray at the target It may miss depending upon the offset
	protected bool FireAtTarget(TargetData targetData, out RaycastHit hitData, out Vector3 direction){
        Vector3 offset = GetTargetOffset(targetData);
		direction = (new Vector3(targetData.location.x,targetData.location.y,targetData.location.z) - muzzlePoint.position).normalized;
        direction += offset;
		return  Physics.Raycast(muzzlePoint.position,direction,out hitData, range);
	}

    // Was the body part hit, based on the probablity 
    protected Vector3 GetTargetOffset(TargetData data) {
        float distance = (target.transform.position - muzzlePoint.position).magnitude;
        // Apply offset to a random axis. 4 is exclusive.
        int randomAxis = Random.Range(1, 4);
        // Add a ranom element
        float randomFactor = Random.Range(1f, 3f);
        // The probability of hitting the targeted body part 
        int prabability = (int)data.probability;
        // calculate offset
        float offset = 0.001f * distance * 1 / prabability * randomFactor;
        if (randomAxis == 1) return new Vector3(offset, 0f, 0f);
        else if (randomAxis == 2) return new Vector3(0f, offset, 0f);
        else return new Vector3(0f, 0f, offset);
    }


}
