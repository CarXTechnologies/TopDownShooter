using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private InputActionAsset m_inputAsset;
		[SerializeField] private CinemachineVirtualCamera m_virtualCamera;
		[SerializeField] private UIPlayerHUD m_playerHUD;
		[SerializeField] private Character m_character;
		[SerializeField] private PlayerProfileSO m_playerProfile;
		[SerializeField] private UpgradeData m_upgradeData;
		
		[Header("Events")]
		[SerializeField] private UnityEvent onPlayerDead;
		
		private MovingComponent m_characterMoving;
		private AttackManager m_attackManager;
		private ManaComponent m_mana;
		private HealthComponent m_health;
		private SearcherTarget m_searcherTarget;

		private InputAction m_moveAction;
		private InputAction m_attackAction;
		private InputAction m_swapWeaponAction;

		private void Awake()
		{
			m_moveAction = m_inputAsset.FindAction("Move");
			m_attackAction = m_inputAsset.FindAction("Fire");
			m_swapWeaponAction = m_inputAsset.FindAction("SwapWeapon");
		}

		private void Start()
		{
			if (m_character)
			{
				Init(m_character);
			}
		}
		public void Init(Character character)
		{
			m_character = character;
			m_character.Init(m_upgradeData.GetCharacterData(m_playerProfile.playerLevel, m_playerProfile.skillsLevel));
			
			m_characterMoving = character.GetComponent<MovingComponent>();
			m_virtualCamera.Follow = character.transform;
			m_attackManager = character.GetComponent<AttackManager>();
			m_mana = character.GetComponent<ManaComponent>();
			m_health = character.GetComponent<HealthComponent>();
			m_searcherTarget = character.GetComponent<SearcherTarget>();
			
			m_health.onDead += onPlayerDead.Invoke;
		}

		private void RefreshUI()
		{
			if (m_playerHUD)
			{
				m_playerHUD.Refresh(m_health.percent, m_mana.percent);
			}
		}

		private void Update()
		{
			if (m_character)
			{
				var move = m_moveAction.ReadValue<Vector2>();
				Vector3 offset = new(move.x, 0f, move.y);
				m_characterMoving.Move(offset);
				var target = m_searcherTarget.Serach();

				if (target)
				{
					var dir = target.position - m_characterMoving.transform.position; 
					m_characterMoving.Look(dir);
				}
				else if (move.x != 0f || move.y != 0f)
				{
					m_characterMoving.Look(offset);
				}

				bool canAttack = m_mana.current >= m_attackManager.needMana && m_attackManager.canAttack;
				if (canAttack)
				{
					if (m_attackAction.WasPressedThisFrame())
					{
						m_attackManager.Attack(target);

						m_mana.Reduce(m_attackManager.needMana);
					}
				}

				if (m_swapWeaponAction.WasPerformedThisFrame())
				{
					m_attackManager.NextSkill();
				}


				RefreshUI();
			}
		}
	}
}